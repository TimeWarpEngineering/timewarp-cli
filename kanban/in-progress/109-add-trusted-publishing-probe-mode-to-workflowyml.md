# Add trusted-publishing probe mode to workflow.yml

## Description

org 458-009 probe (NuGet has no policy-enumeration API; probe = dispatch mode that runs only the nuget/login OIDC exchange and stops — success proves the workflow.yml policy matches; reference timewarp-nuru's workflow.yml).

## Checklist

- [x] probe input added
- [x] login step condition extended
- [x] probe-result step added
- [x] pipeline step skipped in probe mode
- [x] YAML valid

## Results

- Added `mode` choice input (`merge`/`probe`, default `merge`) to `workflow_dispatch` (was bare, no inputs).
- Extended the NuGet login step's `if:` with `|| (github.event_name == 'workflow_dispatch' && inputs.mode == 'probe')`.
- Added a "Trusted publishing probe result" step immediately after login, gated on probe mode.
- Gated the "Run CI Pipeline" step to skip when `github.event_name == 'workflow_dispatch' && inputs.mode == 'probe'`.
- Upload Artifacts step already had `if-no-files-found: ignore` — left untouched, no gating needed.
- YAML validated with `python3 -c "import yaml; yaml.safe_load(...)"` — passed.

### How to validate

**Smoke:** `gh workflow run workflow.yml -f mode=probe` after push → expect the "Trusted publishing probe result" step to run and go green.
**Expect:** a failure of the NuGet login step means the trusted-publishing policy is missing or misconfigured on NuGet.org for this repo + workflow.yml — not a bug in this change.
