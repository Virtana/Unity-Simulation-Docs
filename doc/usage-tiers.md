# Usage Tiers

Tiers in the Unity Simulation product allow us to provide a valuable user experience regardless of your organization’s subscription level.

If any of the below terminology is unfamiliar, please refer to the [taxonomy](taxonomy.md).

Below are the limits of the different tiers, which are applied at the level of your project's Unity organization.

|   | __Free__ | __Premium__  |
|---|:---:|:---:|
| __Sys Params CPU size limit__ | gcp@cpu:6 | None |
| __Maximum simultaneous Run Executions per Unity organization__ | 1 | 100 |
| __Maximum number of Run instances per Run Execution__ | 10 | 2000 |
| __Monthly egress bandwidth limit__ | 10 GiB | 100 GiB |
| __Single Run instance duration limit__ | 1 hour | 4 hours |


To summarize: a free tier organization will have the ability to start at most 1 execution with `gcp@cpu:6` sys-param, having a combined total of 10 instances and generate at most 10 GiB of outbound network traffic per month.

