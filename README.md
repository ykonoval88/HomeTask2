# HomeTask2

|                                Method |    Mean |    Error |   StdDev |  Median |
|-------------------------------------- |--------:|---------:|---------:|--------:|
|      TestAssociatedContainerWithHash1 | 1.385 s | 0.0276 s | 0.0271 s | 1.374 s |
|      TestAssociatedContainerWithHash2 | 1.626 s | 0.0100 s | 0.0093 s | 1.624 s |
|      TestAssociatedContainerWithHash3 | 5.390 s | 0.1860 s | 0.5483 s | 5.104 s |
| TestAssociatedContainerWithCollisions | 3.704 s | 0.0531 s | 0.0470 s | 3.700 s |

TestAssociatedContainerWithHash1 hash = 101 * ((x >> 24) + 101*((x >> 16)+101*(x >> 8)))+x
TestAssociatedContainerWithHash2 hash = ((x >> 16) ^ x) * 0x45d9f3b
TestAssociatedContainerWithHash3 hash = x
TestAssociatedContainerWithCollisions hash = rnd(1, 20000)

Summary

The TestAssociatedContainerWithHash3 where hash = x uses array shown a result with Median time greater than in all other benchmark tests.

A lot of collisions also affects performance, because we extend storage on the same position many times.
