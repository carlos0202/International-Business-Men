import * as React from 'react';
import { roundNumber } from '../utils/math';
import { convertTransactions } from '../utils/transformations';
import * as TransactionsStore from '../store/Transactions';
import * as CurrencyRatesStore from '../store/CurrencyRates';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';

interface TransactionsTableState {
    currentPage: number;
    convertCurrency: string;
}

type TransactionsTableProps = any;

class TransactionsTable extends React.PureComponent<TransactionsTableProps, {}, { currentPage: number, convertCurrency: string }> {
    public state = {
        currentPage: 0
    };

    handleClick(e: any, index: number) {
        e.preventDefault();
        this.setState({
            currentPage: index
        });
    }

    render() {
        const pageSize = 50;
        const { currentPage } = this.state;
        const { transactions, currencyRates, convertCurrency } = this.props;

        let index = 1;
        let pagesCount = Math.ceil(transactions.length / pageSize);
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
                    <Pagination aria-label="Transactions table pagination">

                        <PaginationItem disabled={currentPage <= 0}>

                            <PaginationLink
                                onClick={e => this.handleClick(e, currentPage - 1)}
                                previous
                                href="#"
                            />

                        </PaginationItem>

                        {[...Array(pagesCount)].map((page, i) =>
                            <PaginationItem active={i === currentPage} key={i}>
                                <PaginationLink onClick={e => this.handleClick(e, i)} href="#">
                                    {i + 1}
                                </PaginationLink>
                            </PaginationItem>
                        )}

                        <PaginationItem disabled={currentPage >= pagesCount - 1}>

                            <PaginationLink
                                onClick={e => this.handleClick(e, currentPage + 1)}
                                next
                                href="#"
                            />

                        </PaginationItem>

                    </Pagination>
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
                            {convertedTransactions
                                .slice(
                                    currentPage * pageSize,
                                    (currentPage + 1) * pageSize
                                )
                                .map((transaction: TransactionsStore.ConvertedTransaction) => {
                                    return (
                                        <tr key={index++}>
                                            <td>{transaction.sku}</td>
                                            <td>{transaction.currency}</td>
                                            <td>{transaction.currency} {roundNumber(transaction.amount)}</td>
                                            <td>{transaction.convertedCurrency} {roundNumber(transaction.convertedAmount)}</td>
                                        </tr>
                                    );
                                })}
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

}

export default TransactionsTable;