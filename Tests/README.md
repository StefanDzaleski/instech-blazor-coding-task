# Unit Tests for instech-blazor-coding-task

This test project contains essential unit tests for all services in the Blazor application.

## Test Framework

- **xUnit**: Testing framework
- **Moq**: Mocking library for dependencies

## Test Coverage

### ✅ PositionService Tests (6 tests)
Core functionality tests for vessel position validation:
- Overlap detection (overlapping vs non-overlapping vessels)
- Anchorage boundary validation (inside vs outside)
- Multiple vessel validation

### ✅ VesselLayoutService Tests (4 tests)
Essential tests for vessel positioning and layout:
- Dimension scaling (20x factor)
- Positioning to the right of anchorage
- Column distribution (round-robin)
- Vertical stacking within columns

### ✅ DragService Tests (4 tests)
Core drag-and-drop functionality tests:
- Previous position saving on drag start
- Position updates during drag move
- Position retention when no overlap
- Snap-back behavior when overlap detected

### ✅ ApiService Tests (3 tests)
Essential API communication tests:
- Successful API response handling
- Error response handling
- Multiple fleet support

## Running the Tests

From the Tests directory:
```bash
dotnet test
```

From the project root:
```bash
dotnet test Tests/instech-blazor-coding-task.Tests.csproj
```

With verbose output:
```bash
dotnet test --verbosity detailed
```

## Test Results

**Total Tests: 17**  
**Passed: 17**  
**Failed: 0**  
**Skipped: 0**  
**Duration: < 75ms**

## Test Structure

```
Tests/
├── Services/
│   ├── PositionServiceTests.cs     # 6 tests
│   ├── VesselLayoutServiceTests.cs # 4 tests
│   ├── DragServiceTests.cs         # 4 tests
│   └── ApiServiceTests.cs          # 3 tests
└── README.md
```

## Key Testing Patterns

1. **Arrange-Act-Assert (AAA)**: All tests follow this clear pattern
2. **Mocking**: HttpClient and IPositionService mocked with Moq
3. **Core Functionality Focus**: Tests cover essential business logic
4. **Fast Execution**: All tests run in < 100ms

## Benefits

✅ **Regression Prevention**: Validates core functionality  
✅ **Documentation**: Tests serve as executable specifications  
✅ **Fast Feedback**: Runs in under 100ms  
✅ **Easy to Maintain**: Focused on essential functionality  

## Continuous Integration

These tests can be easily integrated into CI/CD pipelines:
```yaml
# Example for GitHub Actions
- name: Run tests
  run: dotnet test Tests/instech-blazor-coding-task.Tests.csproj
```

