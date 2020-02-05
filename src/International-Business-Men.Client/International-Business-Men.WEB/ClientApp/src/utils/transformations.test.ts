import {
    convertTransactions,
    fillCurrencyTable,
    getMissingConversions,
    getSupportedCurrencies
} from './transformations';
import {
    transactions, currencyRates
} from '../../test/setup/test-data.json';

describe('Transformations utils tests.', () => {
    let _transactions = [];
    let _currencyRates = [];

    beforeAll(() => {
        _transactions = Array.from(_transactions);
        _currencyRates = JSON.parse(JSON.stringify(currencyRates));
    });

    it('should convert transactions correctly', () => {
        // Arrange
        let targetCurrency = 'EUR';
        let sku = 'T2006';
        let expectedFirst = 7.36;
        let expected = 14.99;

        // Act
        let convertedTransactions =
            convertTransactions(targetCurrency, transactions, currencyRates);
        let sumAmount = convertedTransactions
            .filter(e => e.sku === sku)
            .reduce((x, y) => x + y.convertedAmount, 0);

        // Assert
        expect(convertedTransactions[0].convertedAmount).toBe(expectedFirst);
        expect(sumAmount).toBe(expected);
    });

    it('should fill currencyRates missing conversions correctly', () => {
        // Arrange
        let currencyTableCount = 12;

        // Act
        let result = fillCurrencyTable(currencyRates.slice());

        // Assert
        expect(result.length).toBe(currencyTableCount);
    });

    it('should get supported currencies correctly', () => {
        // Arrange
        let currencyTableCount = 4;

        // Act
        let result = getSupportedCurrencies(_currencyRates);

        // Assert
        expect(result.size).toBe(currencyTableCount)
        expect(new Set<string>(result).size).toBe(currencyTableCount);
    });

    it('should get missing conversions correctly', () => {
        // Arrange
        let currencyTableCount = 4;

        // Act
        let supportedCurrencies = getSupportedCurrencies(currencyRates);
        let result = getMissingConversions(_currencyRates, supportedCurrencies);

        // Assert
        expect(result.length).toBe(currencyTableCount);
    });
});