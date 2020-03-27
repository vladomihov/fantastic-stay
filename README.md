# FantasticStay .Net Developer Task

## Used architecture and patterns
- Doamin-Driven Development
- CQRS
- Mediator
- Entities, Object Values and Enumerations
- Aggregate Root
- Unit or Work
- Event Sourcing
- Domain Model following the Persistence Ignorance and Infrastructure Ignorance principals

## TODO list:
### Performace
- Add a 'cache/snapshot' table for the calendar dates
- Use Domain Events to trigger sync after a calendar item is saved to update the calendar dates cache
- Use simple framework like Dapper to extract the calendar availability
- Add Idempotency to the commands

### Quality
- Plug Behaviors in the MediatR pipeline for Validation and Logging
- Add comments and descriptions in the code

### Road map
- Use Domain Events to publish Integration Events
- Add localization for the user masages (FantasticStayDomainException)
- Add Docker support
- Add cloud support like DB and Configuration stores
