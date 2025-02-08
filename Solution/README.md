## Folder Structure
- Core: interface, abstract
- API:
- ComponentContext: Run environment like: PostGre, Kafka, ELK
- Pulse.Library:
  + Core: interface, abstract,...
    - Actions: Action Framework
    - Persistence
      + Models: Action, Entity, Constraint that is used by CLI/Generator and Services and Persistence
  + Common: common class, helper
  + Actions: Action Framework
  + Services
  + Persistence
  + CLI:
    - Generator:
      + The Rule Context Generator uses the schemas from the Masterdata database and the AODB database to generate a strongly typed interface to the data
      + Models in CLI/Generator/Models are represent data of **schema/init.json** file

## Things need to clone:
```
[] - Updater(Generator, Schema)
     + Generate model
     + generate complexDataType => done
     + generate context (query to database)
     + generate mock context
[] - Persistence
[] - Core (MEE)
[] - Configuration.PP (Action Framework & Pipeline)
[] - QueryStream (BE & UI)
[] - CoreUI
[] - UI Modules
[] - Permission (BE & UI)
[] - Terraform (kibana & prometheus & grafana)
```



 // only use class for complexAttributes, not use interface
        // write extension AddValue() for collection
        // ComplexAttributeCollection<ProjectRole> = ProjectRole[]