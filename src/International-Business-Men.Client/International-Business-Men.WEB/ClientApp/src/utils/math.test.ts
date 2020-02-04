import { roundNumber, getConversionAmount, getConversion } from './math';
import { transactions, currencyRates, testRoundNumberData } from '../../test/setup/test-data.json';

describe('Math utils tests.', () => {
    it('should round 2 decimals correctly', () => {
        // Arrange
        let num = testRoundNumberData[0];
        let expected = 15.04;

        // Act
        let result = roundNumber(num);

        // Assert
        expect(result).toBe(expected);
    });

    it('should round 2 decimals handling edge cases >= 5', () => {
        // Arrange
        let num = testRoundNumberData[1];
        let expected = 16.56;

        // Act
        let result = roundNumber(num);

        // Assert
        expect(result).toBe(expected);
    });

    it('Should return correct direct conversion amount', () => {
        // Arrange
        let trans = transactions[2];
        let targetCurrency = 'EUR'; // AUD -> EUR
        let expectedAmount = 9.12;

        // Act
        let result = getConversionAmount(trans, targetCurrency, currencyRates);

        // Assert
        expect(result.convertedAmount).toBe(expectedAmount);
        expect(result.convertedCurrency).toBe(targetCurrency);
    });

    it('Should return correct missing conversion using existing table', () => {
        // Arrange
        let conv = {
            from: 'AUD',
            to: 'EUR',
            rate: 1
        };
        let sourceCurrency = 'AUD';
        let targetCurrency = 'EUR'; // AUD -> EUR
        let expectedRate = 0.50784;
        let paramArray = Array.from(currencyRates);

        // Act
        let result = getConversion(conv, sourceCurrency, targetCurrency, paramArray);

        // Assert
        expect(result.rate).toBe(expectedRate);
        expect(paramArray).toStrictEqual(currencyRates);
    });

    it('Should return undefined on any missing or empty property', () => {
        // Arrange
        let conv = {
            from: 'AUD',
            to: 'EUR',
            rate: 1
        };
        let sourceCurrency = 'AUD';
        let targetCurrency = 'EUR'; // AUD -> EUR
        let expectedResult = undefined;
        let paramArray = Array.from(currencyRates);

        // Act
        let result =
            getConversion(undefined, sourceCurrency, targetCurrency, paramArray) || // missing conversion
            getConversion(conv, undefined, targetCurrency, paramArray) || // missing sourceCurrency
            getConversion(conv, sourceCurrency, undefined, paramArray) || // missing targetCurrency
            getConversion(conv, sourceCurrency, targetCurrency, undefined) || // missing currencyTable
            getConversion(undefined, undefined, undefined, undefined); // missing all required values

        // Assert
        expect(result).toBe(expectedResult);
        expect(paramArray).toStrictEqual(currencyRates);
    });
});