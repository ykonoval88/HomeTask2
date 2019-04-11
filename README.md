# HomeTask2

|                                Method |     Mean |    Error |   StdDev |
|-------------------------------------- |---------:|---------:|---------:|
|      TestAssociatedContainerWithHash1 |  4.912 s | 0.0498 s | 0.0416 s |
|      TestAssociatedContainerWithHash2 |  1.940 s | 0.0401 s | 0.1137 s |
|      TestAssociatedContainerWithHash3 |  5.031 s | 0.1017 s | 0.2054 s |
| TestAssociatedContainerWithCollisions | 14.819 s | 0.2932 s | 0.4817 s |


TestAssociatedContainerWithHash1 hash = 101 * ((x >> 24) + 101*((x >> 16)+101*(x >> 8)))+x
TestAssociatedContainerWithHash2 hash = ((x >> 16) ^ x) * 0x45d9f3b
TestAssociatedContainerWithHash3 hash = x
TestAssociatedContainerWithCollisions hash = rnd(1, 20000)

Summary

- The TestAssociatedContainerWithHash3 where hash = x and TestAssociatedContainerWithHash1 thar are used array (the small gaps) shown a result with Median time greater than benchmark for TestAssociatedContainerWithHash2 where we have a gap, but not a lot of collisions .

- A lot of collisions also affects performance, because we extend storage on the same position many times and should rebase hash table   when a lot of items refers to the same hash code.

- I would prefer hash = ((x >> 16) ^ x) * 0x45d9f3b, because it includes a +/- bit in hash generation.
