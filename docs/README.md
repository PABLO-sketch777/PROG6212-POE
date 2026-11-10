# Part 1 planning evidence
The ERD and SQL schema define the six entities used by the Part 2 EF Core model. The endpoint plan is the implementation contract. The API intentionally routes file metadata through `BannerImageUrl`; Part 3 can add an API-owned multipart upload endpoint backed by Azure Blob Storage without coupling MVC directly to Azure.

Render the ERD with `manus-render-diagram docs/erd.mmd docs/erd.png`.
