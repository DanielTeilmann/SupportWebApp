# SupportWebApp

## Formål

Formålet med projektet er at lave en simpel webapplikation, hvor brugere kan oprette supporthenvendelser.

Applikationen giver brugeren mulighed for at:

- Oprette en supporthenvendelse
- Indtaste sine oplysninger som er : navn, email, telefonnummer, beskrivelse og kategori
- Se supporthenvendelser som er lavet
- Gemmer suporthenvendelser i Azure Cosmos DB så de kan findes og vises igen på en oversigtsside.
- Validere inputtet fra brugeren

## Oprettelse af Azure Cosmos DB

Først logger man ind på Azure:

```bash
az login
```

Derefter opretter man en resource group:
(jeg har brugt francecenral som region)

```bash
az group create --name IBasSupportRG --location francecentral
```

Herefter opretter man en Cosmos DB account:

```bash
az cosmosdb create \
  --name ibas-db-account \
  --resource-group IBasSupportRG \
  --locations regionName=francecentral \
  --enable-free-tier true
```

Derefter oprettes databasen:

```bash
az cosmosdb sql database create \
  --account-name ibas-db-account \
  --resource-group IBasSupportRG \
  --name IBasSupportDB
```

Til sidst oprettes containeren med category som partition key:

```bash
az cosmosdb sql container create \
  --account-name ibas-db-account \
  --resource-group IBasSupportRG \
  --database-name IBasSupportDB \
  --name ibassupport \
  --partition-key-path "/category"
```

Connection string kan hentes med:

```bash
az cosmosdb keys list \
  --name ibas-db-account \
  --resource-group IBasSupportRG \
  --type connection-strings
```

Connection stringen til databasen gemmes i User Secrets, så de følsomme oplysninger ikke bliver uploadet til GitHub.

Hvis projektet skal køres lokalt på en ny computer, skal Cosmos DB Connection String derfor tilføjes til User Secrets.

```bash
dotnet user-secrets init
dotnet user-secrets set "CosmosDb:ConnectionString" "DIN_AZURE_COSMOSDB_CONNECTION_STRING"
```


## Status

Jeg fik lavet alle aktiviteter/opgaver som opgaven krævede

## Hvad mangler? / Næste trin


Som næste trin synes jeg, at det kunne være relevant at lave mulighed for at redigere og slette henvendelser.

Det kunne også være relevant at lave en mulighed for søgning eller en form for filtrering så det bliver mere overskueligt
og nemmere at finde bestemte supporthendvendelser


