import { roundNumber, getConversionAmount } from './math';

const testRoundNumberData = [15.044, 16.555];
const transactions = [
    { "sku": "T2006", "amount": 10.00, "currency": "USD" },
    { "sku": "M2007", "amount": 34.57, "currency": "CAD" },
    { "sku": "R2008", "amount": 17.95, "currency": "AUD" },
    { "sku": "T2006", "amount": 7.63, "currency": "EUR" },
    { "sku": "B2009", "amount": 21.23, "currency": "USD" }
];
// missing AUD to EUR conversion.
const currencyRates = [
    { "from": "EUR", "to": "USD", "rate": 1.359 },
    { "from": "CAD", "to": "EUR", "rate": 0.732 },
    { "from": "USD", "to": "EUR", "rate": 0.736 },
    { "from": "EUR", "to": "CAD", "rate": 1.366 },
    { "from": "USD", "to": "AUD", "rate": 1.45 },
    { "from": "AUD", "to": "USD", "rate": 0.69 },
    { "from": "CAD", "to": "AUD", "rate": 1.96 }
];

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
});