# SurvivorWaveShooter

Unity FPS wave-shooter project, developed by two people (not a solo project).

## Working agreements

- **MegaBrain (Claude) is responsible for code optimization** in this project — both runtime performance (GC allocations, Update-loop cost, physics/raycast/NavMesh frequency, etc.) and code quality (dead code, unclear naming, unnecessary duplication). Treat this as a standing responsibility: flag and fix optimization opportunities whenever touching a script, not just when explicitly asked.
- Since this is a 2-person team, favor code that's easy for a teammate to pick up: avoid unexplained cleverness, keep changes scoped and reviewable, don't leave debug scaffolding (stray `Debug.Log`, dead fields) behind.
