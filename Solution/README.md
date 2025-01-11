- Core: interface, abstract
- Infrastructure: implement Core defined like database, adapter,...
- API
- Service:
- ComponentContext: Run environment like: PostGre, Kafka, ELK
- Pulse.Library:
  + Core: interface, abstract
  + Common: common class, helper
  + Action: Action Framework
  + CLI: 
    - Generator: 
      + The Rule Context Generator uses the schemas from the Masterdata database and the AODB database to generate a strongly typed interface to the data
      + Models in CLI/Generator/Models are represent data of **schema/init.json** file