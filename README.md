# README #

This is the werkdagen API. The API has 1 method. This method can be used to calculate a working day in the past or future, starting from the current date. If no range is set then the service will return the last workday in the past. If a negative range x is set then the service will return the x-th last workday in the past. If a positive range x is set then the service will return the x-th next workday in the present. 

A workday is a day that is neither in a weekend or on a holliday. 

## Features ##

* Get workday is used to return the last workday in the past from today's date. If the current date is a workday then the current date will be returned. 
* Get workday with a range is used to return either the last or the next workday in the past or future starting from the current date that is within the given range. 

## Dependencies

This service uses a list with dates that are holidays. The list is provided in Excel format and the first column contains the dates that match with hollidays. 

## Building the source code ##

### Locally ###

### S2I ###

### Docker ###

## Running the application ##

### Environment variables ###

* AppNamespacePrefix: used to provide the API with a prefix in the URI
* Excell__ExcellFilePath: path to the Excel file containing the hollidays. This file should be provided in the container. 
* Excell__DateInColumn: Used to indicate the column where the hollidays are in.
* Serilog__MinimumLevel__Default: Used to increase/decrease the logging level of the application. 
* ELASTIC_APM_SECRET_TOKEN: Needed when using APM
* ELASTIC_APM_SERVER_URL: Needed when using APM
* ELASTIC_APM_VERIFY_SERVER_CERT: Needed when using APM
* ELASTIC_APM_CENTRAL_CONFIG: Needed when using APM
* ELASTIC_APM_SERVICE_NAME: Needed when using APM
* ELASTIC_APM_ENVIRONMENT: Needed when using APM

## Testing the application ##

Run integration tests using the [Karate framework](https://github.com/intuit/karate). See the [testing documentation](./karate/README.md) on how to run the tests locally and more information. 