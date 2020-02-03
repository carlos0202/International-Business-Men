import * as React from 'react';
import { roundNumber } from '../utils/math';
import { convertTransactions } from '../utils/transformations';
import * as TransactionsStore from '../store/Transactions';

const TransactionsTable = (props: any) => {
    let { transactions, currencyRates, convertCurrency } = props;
    let index = 1;
    let convertedTransactions = convertTransactions(convertCurrency, transactions, currencyRates);
    let totalAmount = convertedTransactions.reduce((total: number, currentTransaction) => {
        return roundNumber(total + currentTransaction.convertedAmount);
    }, 0);

    if (convertedTransactions.length) {
        return (
            <React.Fragment>
                <div className="row">
                    <div className="col-lg-10 col-lg-offset-1">
                        <h4>Total transacciones: {convertCurrency} {totalAmount}</h4>
                    </div>
                </div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>SKU del Producto</th>
                            <th>Moneda</th>
                            <th>Monto Transacci&oacute;n</th>
                            <th>Monto Convertido</th>
                        </tr>
                    </thead>
                    <tbody>
                        {convertedTransactions.map((transaction: TransactionsStore.ConvertedTransaction) => {
                            return (
                                <tr key={index++}>
                                    <td>{transaction.sku}</td>
                                    <td>{transaction.currency}</td>
                                    <td>{transaction.currency} {roundNumber(transaction.amount)}</td>
                                    <td>{transaction.convertedCurrency} {roundNumber(transaction.convertedAmount)}</td>
                                </tr>
                            );
                        }
                        )}
                    </tbody>
                </table>
            </React.Fragment>
        );
    } else {
        return (
            <div>No hay datos para mostrar!</div>
        );
    }
}

export default TransactionsTable;