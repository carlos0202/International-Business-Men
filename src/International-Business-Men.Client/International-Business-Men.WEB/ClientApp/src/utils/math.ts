import { Transaction, ConvertedTransaction } from '../store/Transactions';
import { CurrencyRate } from '../store/CurrencyRates';

export const roundNumber = (n: number, precision: number = 2) => {
    return Math.round((n + Number.EPSILON) * Math.pow(10, precision)) / Math.pow(10, precision);
}

export const getConversionAmount = (
    transaction: Transaction | ConvertedTransaction,
    targetCurrency: string,
    currencyTable: CurrencyRate[],
    usedCurrencies: Set<string> = new Set<string>(),
    conversionRate: number = 1,
    currentDepth: number = 1): ConvertedTransaction | undefined => {

    if (transaction.currency === targetCurrency) {
        return {
            ...transaction,
            convertedAmount: transaction.amount,
            conversionRate: conversionRate,
            convertedCurrency: targetCurrency
        };
    }

    // Maximum navigation while doing conversions.
    if (currentDepth > 10) return undefined;

    let currencyRate = currencyTable
        .find(e => e.from === transaction.currency && e.to === targetCurrency);
    if (currencyRate) {
        conversionRate *= currencyRate.rate;
        let result = transaction.amount * conversionRate;

        let outTransaction = {
            ...transaction,
            convertedAmount: roundNumber(result, 2),
            conversionRate: conversionRate,
            convertedCurrency: targetCurrency
        };

        return outTransaction;
    } else {
        let currencyRate = currencyTable
            .find(e => e.from === transaction.currency && !usedCurrencies.has(e.to));
        usedCurrencies.add(targetCurrency);

        if (!currencyRate) return undefined;

        conversionRate *= currencyRate.rate;
        currentDepth++;

        let convertedTransaction = {
            ...transaction,
            convertedAmount: 0,
            conversionRate: conversionRate,
            convertedCurrency: currencyRate.to,
            currency: currencyRate.to
        };

        return getConversionAmount(convertedTransaction, targetCurrency, currencyTable, usedCurrencies, conversionRate, currentDepth);
    }
}