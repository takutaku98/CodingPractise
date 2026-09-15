# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this repo is

A personal LeetCode practice repo (.NET 10 / C#). Each solved problem gets a solution, a Japanese-language writeup explaining the approach, and tests (example-based + property-based). The writeups are the primary artifact — this repo doubles as a study log, not just a code exercise.

## Commands

```bash
dotnet test                                            # run all tests
dotnet test --filter "FullyQualifiedName~TwoSum"       # run a single problem's tests
dotnet build                                            # build only
```

### Docker (no local .NET SDK needed)

```bash
docker compose run --rm test                                              # run all tests
docker compose run --rm test --filter "FullyQualifiedName~TwoSum"         # filtered
docker compose run --rm debug                                             # run with vsdbg, waits for VS Code attach ("Docker: attach to testhost")
```

Dockerfile is multi-stage: `build` (restore+build, csproj copied first for layer caching) → `test` / `debug`.

## Structure and conventions

- `LeetCode/{番号}-{問題名}/Solution.cs` — solution code. Each problem's `Solution` class lives in its own namespace (e.g. `LeetCode.TwoSum`) because the class name `Solution` repeats across problems.
- `LeetCode/{番号}-{問題名}/README.md` — Japanese explanation of the approach: problem statement, comparison to the naive solution, core idea, a worked trace table, complexity, and a "テスト" section describing the test cases. New problems should follow this same structure (see `LeetCode/0001-TwoSum/README.md` for the template).
- `LeetCode.Tests/{番号}-{問題名}/{Name}Tests.cs` — mirrors the same numbered folder structure, namespaced `LeetCode.Tests.{ProblemName}`.
- Top-level `README.md` has a problem index table (`# | 問題 | 解説`) — add a row here when adding a new problem.

### Test pattern per problem

Every problem gets exactly two kinds of tests:
1. `[Theory]` + `[InlineData]` — a handful of concrete cases chosen to cover known edge cases (e.g. duplicate values, negatives, avoiding self-pairing).
2. `[Property]` (FsCheck.Xunit) — one property-based test. The usual shape: derive an input that's guaranteed to have a valid answer (e.g. plant a pair and compute `target` from it), then assert the *property* the result must satisfy rather than the exact expected value, since multiple valid answers may exist.

Comments in solution code and explanations in READMEs are written in Japanese; identifiers (class/method/variable names) are in English.
