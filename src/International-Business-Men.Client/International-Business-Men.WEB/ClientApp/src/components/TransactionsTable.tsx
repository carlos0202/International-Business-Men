import * as React from 'react';
import { roundNumber } from '../utils/math';
import { convertTransactions } from '../utils/transformations';
import * as TransactionsStore from '../store/Transactions';
import * as CurrencyRatesStore from '../store/CurrencyRates';
import { Pagination, PaginationItem, PaginationLink, Input } from 'reactstrap';
import LoadingSpinner from './LoadingSpinner';

type TransactionsTableProps = {
    currentPage: number;
    convertCurrency: string;
    transactions: TransactionsStore.Transaction[],
    currencyRates: CurrencyRatesStore.CurrencyRate[],
    isLoading: boolean
};

class TransactionsTable extends React.PureComponent<any, {}, { currentPage: number, convertCurrency: string, pageSize: number }> {
    public state = {
        currentPage: 0,
        pageSize: 10
    };

    handleClick(e: any, index: number) {
        e.preventDefault();
        this.setState({
            currentPage: index
        });
    }

    handlePageSizeChange(e: any) {
        e.preventDefault();
        this.setState({
            pageSize: e.target.value,
            currentPage: 0
        });
    }

    render() {
        const pageSize = this.state.pageSize;
        const { currentPage } = this.state;
        const { transactions, currencyRates, convertCurrency, isLoading } = this.props;

        if ((!transactions || !currencyRates) || !(transactions.length || !currencyRates.length) || isLoading) {           
            return (
                <div><LoadingSpinner /></div>
            );
        } else {
            let index = 1;
            let pagesCount = Math.ceil(transactions.length / pageSize);
            
            let convertedTransactions = convertTransactions(convertCurrency, transactions, currencyRates);
            let totalAmount = convertedTransactions.reduce((total: number, currentTransaction) => {
                return roundNumber(total + currentTransaction.convertedAmount);
            }, 0);

            return (
                <React.Fragment>
                    <div className="row">
                        <div className="col-md-6 d-flex justify-content-start">
                            <h4>Total transacciones: {convertCurrency} {totalAmount}</h4>
                        </div>
                        <div className="col-md-2 d-flex justify-content-end">
                            <Input type="select" name="pageSize" id="convertCurrency" onChange={this.handlePageSizeChange.bind(this)}
                                value={this.state.pageSize} placeholder="Registros por página">
                                <option value={10}>10</option>
                                <option value={50}>50</option>
                                <option value={100}>100</option>
                                <option value={transactions.length}>Todos</option>
                            </Input>
                        </div>
                        <div className="col-md-4 d-flex justify-content-end">
                            <Pagination aria-label="Transactions table pagination">
                                <PaginationItem disabled={currentPage <= 0}>
                                    <PaginationLink
                                        onClick={e => this.handleClick(e, 0)}
                                        first
                                        href="#"
                                    />
                                </PaginationItem>
                                <PaginationItem disabled={currentPage <= 0}>
                                    <PaginationLink
                                        onClick={e => this.handleClick(e, currentPage - 1)}
                                        previous
                                        href="#"
                                    />
                                </PaginationItem>
                                <PaginationItem active={true}>
                                    <PaginationLink onClick={e => this.handleClick(e, currentPage + 1)} href="#">
                                        {currentPage + 1}
                                    </PaginationLink>
                                </PaginationItem>
                                <PaginationItem disabled={currentPage >= pagesCount - 1}>
                                    <PaginationLink
                                        onClick={e => this.handleClick(e, currentPage + 1)}
                                        next
                                        href="#"
                                    />
                                </PaginationItem>
                                <PaginationItem disabled={currentPage >= pagesCount - 1}>
                                    <PaginationLink
                                        onClick={e => this.handleClick(e, pagesCount - 1)}
                                        last
                                        href="#"
                                    />
                                </PaginationItem>
                            </Pagination>
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
        }
    }

}

export default TransactionsTable;