# ---- build: 復元とビルド(レイヤーキャッシュのため csproj を先にコピー) ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY CodingPractise.slnx ./
COPY LeetCode/LeetCode.csproj LeetCode/
COPY LeetCode.Tests/LeetCode.Tests.csproj LeetCode.Tests/
RUN dotnet restore CodingPractise.slnx

COPY . .
RUN dotnet build CodingPractise.slnx -c Debug --no-restore

# ---- test: テスト実行用(docker compose run --rm test) ----
FROM build AS test
ENTRYPOINT ["dotnet", "test", "--no-build", "--logger", "console;verbosity=normal"]

# ---- debug: テストデバッグ用。vsdbg 入り + testhost がデバッガ接続を待つ ----
FROM build AS debug
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl unzip procps \
    && rm -rf /var/lib/apt/lists/* \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg
# testhost 起動時に PID を表示してデバッガのアタッチを待つ
ENV VSTEST_HOST_DEBUG=1
ENTRYPOINT ["dotnet", "test", "--no-build"]
