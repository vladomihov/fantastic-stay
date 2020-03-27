# FantasticStay .Net Developer Task

## TODO list:
### Performace
- Add a 'cache' table for the calendar dates
- Use Domain Events to trigger sync after a calendar item is saved to update the calendar dates cache
- Use simple framework like Dapper to extract the calendar availability
- Add Idempotency to the commands

### Quality
- Plug Behaviors in the MediatR pipeline for Validation and Logging
- Add comments and descriptions in the code

### Road map
- Use Domain Events to publish Integration Events
- Add localization for the user masages (FantasticStayDomainException)
