import { Transaction } from '../store/Transactions';
import { CurrencyRate } from '../store/CurrencyRates';

export const roundNumber = (n: number, precision: number = 2) => {
    return Math.round((n + Number.EPSILON) * Math.pow(10, precision)) / Math.pow(10, precision);
}

export const getConversionAmount = (
    transaction: Transaction,
    targetCurrency: string,
    currencyTable: CurrencyRate[],
    usedCurrencies: Set<string> = new Set<string>(),
    currentDepth: number = 1): number => {
    usedCurrencies.add(targetCurrency);
    let currencyRate = currencyTable
        .find(e => e.from === transaction.currency && e.to === targetCurrency);
    if (currencyRate) {
        let result = transaction.amount * currencyRate.rate;

        return roundNumber(result, 2);
    } else {
        currencyRate = currencyTable
            .find(e => e.from === transaction.currency && !usedCurrencies.has(e.to));

        return getConversionAmount(transaction, targetCurrency, currencyTable, usedCurrencies);
    }
}