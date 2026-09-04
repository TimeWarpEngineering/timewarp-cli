# Round 2 — general
**Date:** 2026-09-04
**Scope reviewed:** post-fix test delta for M1/M2 (direct.remove-item.cs, direct.get-child-item.cs)

## Summary

M1 and M2 both hold. The three symlink-safety tests in `direct.remove-item.cs` now throw `InvalidOperationException` (naming Unix / Windows Developer Mode) when `CreateSymbolicLink` fails, so they no longer vacuous-pass. The new `direct.get-child-item.cs` mirrors the GetContent cancellation pattern (cancel after first of five entries, assert ≥1 read, try/finally cleanup), uses `Direct_` / `ModuleInitializer` registration, and is picked up by the multi-file runner glob. No new defects found in the fix delta.

## Prior findings

- M1 — CONFIRMED fixed — All three symlink tests rethrow link-creation failures as `InvalidOperationException` instead of silent `return`.
- M2 — CONFIRMED fixed — `Cancellation_Should_ThrowOperationCanceledException` present in new `direct.get-child-item.cs` with multi-entry cancel-after-first coverage.

## Issues
