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
});