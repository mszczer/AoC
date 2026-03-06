[![Build and Test](https://github.com/mszczer/AoC/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mszczer/AoC/actions/workflows/dotnet.yml)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/95ac6c88fad04c6fb2c4b4fdec33cf7b)](https://app.codacy.com/gh/mszczer/AoC/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/95ac6c88fad04c6fb2c4b4fdec33cf7b)](https://app.codacy.com/gh/mszczer/AoC/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_coverage)
![License](https://img.shields.io/github/license/mszczer/AoC)
![Platform](https://img.shields.io/badge/platform-.NET-512BD4)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/mszczer/AoC)
![GitHub last commit](https://img.shields.io/github/last-commit/mszczer/AoC)
![GitHub top language](https://img.shields.io/github/languages/top/mszczer/AoC)

# Advent of Code (C# / .NET)

Solutions to [Advent of Code](https://adventofcode.com/) puzzles implemented in **C#** with a focus on **clarity**, **testability**, and **consistent structure**—organized **by year**.

---

<a id="table-of-contents"></a>
## Table of Contents
- [Purpose / Vision](#purpose-vision)
- [Project Structure](#project-structure)
- [Solutions Index](#solutions-index)
- [How to Run / Test](#how-to-run--test)
- [Tech Stack](#tech-stack)
- [Related Resources / Inspiration](#related-resources--inspiration)
- [License](#license)

---

<a id="purpose-vision"></a>
## Purpose / Vision
The goal of this repository is to maintain a high-signal AoC archive that’s easy to revisit:
predictable naming, consistent patterns, and (where practical) automated tests.

---

<a id="project-structure"></a>
## Project Structure
```text
AoC/
├─ AoC2022/                       # 2022 solutions
│  └─ InputData/                  # puzzle inputs (Day01.txt ... Day25.txt)
├─ AoC2022.Test/                  # 2022 tests
│  └─ InputData/                  # test inputs (TestDay01.txt ... TestDay25.txt)
├─ AoC2023/                       # 2023 solutions
│  └─ InputData/
├─ AoC2023.Test/                  # 2023 tests
│  └─ InputData/
├─ AoC2024/                       # 2024 solutions
│  └─ InputData/
├─ AoC2024.Tests/                 # 2024 tests
│  └─ InputData/
├─ AoC2025/                       # 2025 solutions
│  └─ InputData/
├─ AoC2025.Tests/                 # 2025 tests
│  └─ InputData/
├─ Common/                        # shared helpers/utilities (if applicable)
├─ .github/workflows/             # CI configuration (GitHub Actions)
└─ README.md
```

---

<a id="solutions-index"></a>
## Solutions Index

> This section is generated.

<!-- AOC_SOLUTIONS_INDEX:START -->
- [2022](AoC2022)
- [2023](AoC2023)
- [2024](AoC2024)
- [2025](AoC2025)
<!-- AOC_SOLUTIONS_INDEX:END -->

---

<a id="how-to-run--test"></a>
## How to Run / Test
```bash
dotnet build
dotnet test
```

---

<a id="tech-stack"></a>
## Tech Stack
- **Language:** C#
- **Platform:** .NET
- **Testing:** NUnit
- **Coverage:** Coverlet (collector)
- **CI:** GitHub Actions
- **Code Quality:** Codacy

---

<a id="related-resources--inspiration"></a>
## Related Resources / Inspiration
- Advent of Code: https://adventofcode.com/
- Microsoft .NET docs: https://learn.microsoft.com/dotnet/

---

<a id="license"></a>
## License
The scripts and documentation in this project are released under the [MIT License](LICENSE).
