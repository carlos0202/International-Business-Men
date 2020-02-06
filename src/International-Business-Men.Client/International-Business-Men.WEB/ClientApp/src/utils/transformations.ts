import { getConversionAmount, getConversion } from '../utils/math';
import * as TransactionsStore from '../store/Transactions';
import * as CurrencyRates from '../store/CurrencyRates';

export const convertTransactions = (
    convertedCurrency: string,
    transactions: TransactionsStore.Transaction[],
    currencyRates: CurrencyRates.CurrencyRate[]): TransactionsStore.ConvertedTransaction[] => {

    return transactions.map((transaction: TransactionsStore.Transaction) => {
        let conv = getConversionAmount(transaction, convertedCurrency, currencyRates);

        return conv
            ? conv
            : {
                ...transaction,
                conversionRate: 0,
                convertedAmount: -1,
                convertedCurrency:
                    convertedCurrency
            };
    });
}

export const getSupportedCurrencies = (currencyTable: CurrencyRates.CurrencyRate[]): Set<string> => {
    let currencies = new Set<string>();

    if (!currencyTable) return currencies;

    currencyTable.forEach(e => {
        currencies.add(e.from);
        currencies.add(e.to);
    });

    return currencies;
}

export const getMissingConversions = (
    currencyRates: CurrencyRates.CurrencyRate[],
    supportedCurrencies: Set<string>,
    missingConversions: Array<CurrencyRates.CurrencyRate> = new Array<CurrencyRates.CurrencyRate>())
    : CurrencyRates.CurrencyRate[] => {
    let allCurrencies = Array.from(supportedCurrencies);

    allCurrencies.forEach(c => {
        let destinationCurrencies = allCurrencies.filter(e => e !== c);
        for (let targetCurrency of destinationCurrencies) {
            if (currencyRates.some(x => x.from === c && x.to === targetCurrency)) {
                continue;
            } else {
                missingConversions.push({
                    from: c,
                    to: targetCurrency,
                    rate: 1
                });
            }
        }
    });
    console.log(missingConversions);
    const copyCurrencies = currencyRates;
    const resolvedConversions = new Array<CurrencyRates.CurrencyRate>();
    for (const element of missingConversions) {
        var t = element;
        const conv = getConversion(t, t.from, t.to, copyCurrencies);

        if (conv) {
            resolvedConversions.push(conv);
        }
    };

    return resolvedConversions;
}

export const fillCurrencyTable = (currencyRates: CurrencyRates.CurrencyRate[], callStack: number = 1)
    : CurrencyRates.CurrencyRate[] => {
    console.log(`sent currencies: ${currencyRates.length}`)
    const supportedCurrencies = getSupportedCurrencies(currencyRates);
    const missingConversions = getMissingConversions(currencyRates, supportedCurrencies, []);
    missingConversions.forEach(i => currencyRates.push(i));
    console.log(currencyRates.length, missingConversions.length);
    let currCount = Array.from(supportedCurrencies).length;
    let possibleCombinations = currCount * currCount - currCount;

    if (currencyRates.length === possibleCombinations || callStack > 5) {
        return currencyRates;
    } else {
        callStack++
        return fillCurrencyTable(currencyRates, callStack);
    }
}