# Stock-Screener-Service
Simple technical stock screener service. Currently works on Warsaw Stock Exchange only.

## Project structure
* Screener - Library with types and logic
* QuoteService - WCF service for stock screening, currently with one request only
* IntegrationTests - AKA Smoke tests
* UnitTests
* ScreenerDto - Service library for transfer objects, currently unused

## Usage
Load soapui project and send request with requested tickers to the service.
The service will filter stocks using current quotes of tickers sent.
The algorytm will show stocks that are about to break 20 day high in rising trends

## Roadmap
1. Cleanup & Refactor
2. More filters & better services
3. WebUI

## Setup guide
TODO

## Contribution guidelines
TODO
