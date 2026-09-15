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

## 問題一覧

| # | 問題 | 解説 |
|---|------|------|
| 1 | Two Sum | [LeetCode/0001-TwoSum](LeetCode/0001-TwoSum/README.md) |
