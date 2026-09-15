# CodingPractise

コーディング問題の解答と復習用解説のリポジトリ。

## 構成

- `LeetCode/` — 解答ライブラリ(.NET 10)。`LeetCode/番号-問題名/` に解答コード(`Solution.cs`)と解説(`README.md`)を置く
- `LeetCode.Tests/` — xUnit テストプロジェクト。1 問につき以下の 2 件を書く
  - `[Theory]` + `InlineData` による具体例 4 件のテスト
  - FsCheck によるプロパティベースドテスト 1 件

問題ごとにクラス名が `Solution` で被るため、名前空間(例: `LeetCode.TwoSum`)で分ける。

## 実行方法

```bash
dotnet test   # 全テスト実行
dotnet build  # ビルドのみ
```

## Docker でのテスト実行

ローカルに .NET SDK がなくても、Docker だけでテストを実行できる。

```bash
# 全テスト実行
docker compose run --rm test

# テストを絞り込んで実行(引数はそのまま dotnet test に渡る)
docker compose run --rm test --filter "FullyQualifiedName~TwoSum"
```

Dockerfile はマルチステージ構成:

| ステージ | 役割 |
|----------|------|
| `build` | restore + build(csproj を先にコピーしてレイヤーキャッシュを効かせる) |
| `test` | `dotnet test` を実行するだけ |
| `debug` | vsdbg(VS Code 用 .NET デバッガ)入り。`VSTEST_HOST_DEBUG=1` で testhost がデバッガのアタッチを待つ |

## Docker でのテストデバッグ(VS Code)

1. デバッグ用コンテナでテストを起動する:

   ```bash
   docker compose run --rm debug
   # 絞り込みも可: docker compose run --rm debug --filter "FullyQualifiedName~KnownCases"
   ```

2. コンソールに以下のように表示されて、testhost がアタッチ待ちで停止する:

   ```
   Host debugging is enabled. Please attach debugger to testhost process to continue.
   Process Id: 65, Name: dotnet
   ```

3. VS Code でブレークポイントを置き、デバッグ構成 **「Docker: attach to testhost」**(`.vscode/launch.json`)を実行。プロセス一覧から上記の Process Id の `dotnet` を選ぶ。

4. アタッチするとテストが続行され、ブレークポイントで停止する。

仕組み: `launch.json` の `pipeTransport` が `docker exec` 経由でコンテナ内の vsdbg に接続し、`sourceFileMap` がコンテナ内のパス `/src` をローカルのワークスペースに対応付けている。VS Code 側には [C# 拡張機能](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)が必要。

## 問題一覧

| # | 問題 | 解説 |
|---|------|------|
| 1 | Two Sum | [LeetCode/0001-TwoSum](LeetCode/0001-TwoSum/README.md) |
