import {  getConversionAmount } from '../utils/math';
import * as TransactionsStore from '../store/Transactions';
import * as CurrencyRates from '../store/CurrencyRates';

export const convertTransactions = (
    convertedCurrency: string,
    transactions: TransactionsStore.Transaction[],
    currencyRates: CurrencyRates.CurrencyRate[]): TransactionsStore.ConvertedTransaction[] => {

    return transactions.map((transaction: TransactionsStore.Transaction) => {
        let conv = getConversionAmount(transaction, convertedCurrency, currencyRates);

        return conv ? conv : { ...transaction, conversionRate: 0, convertedAmount: -1, convertedCurrency: convertedCurrency };
    });
}