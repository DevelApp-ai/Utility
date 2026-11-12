# Utility

Contains several utility classes used in several projects. Published as the `DevelApp.Utility` NuGet package.

License: MIT — © DevelApp.ai ApS.

## Model types

- `KeyString` — a string key with a maximum length of 100 characters.
- `IdentifierString` — an ASCII identifier: a letter followed by letters or numbers.
- `NamespaceString` — a dot-separated namespace, translatable to a file path.
- `SemanticVersionNumber` — a `major.minor.patch` version with ordering and equality.
- `CodeBuilder` — a chained, indent-aware code generator.

## Equality semantics

All model types implement value equality (`Equals`, `GetHashCode`, `==`, `!=`):
two instances holding the same value are equal and produce the same hash code,
so lookups in `Dictionary`/`HashSet` succeed with freshly created instances.
(The historic `TryGetValue` dictionary lookup failure was caused by a missing
`GetHashCode` override and is fixed.)