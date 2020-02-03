import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as TransactionsStore from '../store/Transactions';
import * as CurrencyRatesStore from '../store/CurrencyRates';
import { RouteComponentProps } from 'react-router';
import {
    Form, InputGroup,
    InputGroupAddon,
    Input,
    InputGroupText
} from 'reactstrap';
import { roundNumber, getConversionAmount } from '../utils/math';
import { bindActionCreators } from 'redux';

// At runtime, Redux will merge together...
type TransactionProps =
    TransactionsStore.TransactionsState
    & CurrencyRatesStore.CurrencyRatesState// ... state we've requested from the Redux store
    & typeof TransactionsStore.actionCreators // ... plus action creators we've requested
    & typeof CurrencyRatesStore.actionCreators
    & RouteComponentProps<{ sku: string }>;

class Transactions extends React.PureComponent<TransactionProps, { }, { sku: string }> {
    public state = {
        sku: ''
    };

    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    onChange = (evt: any) => this.setState({ sku: evt.target.value });

    onSubmit = (e: any) => {
        e.preventDefault();
        let sku = this.state.sku ? this.state.sku : "";
        this.props.history.replace(`/Transactions/${sku}`, "urlHistory");
        this.props.requestTransactions(sku || "");
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Transacciones</h1>
                <Form onSubmit={this.onSubmit.bind(this)}>
                    <InputGroup>
                        <Input type="text" id="sku" onChange={this.onChange.bind(this)} placeholder="SKU del Producto" />
                        <InputGroupAddon addonType="append" onClick={this.onSubmit.bind(this)}>
                            <InputGroupText>Buscar</InputGroupText>
                        </InputGroupAddon>
                    </InputGroup>
                </Form>
                <br />
                <br />
                {this.renderTransactionsTable()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        //this.props.requestCurrencyRates();
        const sku = this.props.match.params.sku || "";
        this.props.requestTransactions(sku);
        this.props.requestCurrencyRates();
    }

    private convertTransactions(convertedCurrency: string): TransactionsStore.ConvertedTransaction[] {
        let { transactions, currencyRates } = this.props;

        return transactions.map((transaction: TransactionsStore.Transaction) => {
            let conv = getConversionAmount(transaction, convertedCurrency, currencyRates);

            return conv ? conv : { ...transaction, conversionRate: 0, convertedAmount: -1, convertedCurrency: convertedCurrency };
        });
    }

    private renderTransactionsTable() {
        let index = 1;
        let convertedTransactions = this.convertTransactions("EUR");
        let totalAmount = convertedTransactions.reduce((total: number, currentTransaction) => {
            return roundNumber(total + currentTransaction.convertedAmount);
        }, 0);

        if (convertedTransactions.length) {
            return (
                <React.Fragment>
                    <div className="row">
                        <div className="col-lg-10 col-lg-offset-1">
                            <h1>Total transacciones: EUR {totalAmount}</h1>
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
}

export default connect(
    (state: ApplicationState) => {
        const { transactions, currencyRates } = state;

        return { ...transactions, ...currencyRates };
    }, // Selects which state properties are merged into the component's props
    (dispatch: any) => {
        return {
            ...bindActionCreators(Object.assign({}, TransactionsStore.actionCreators, CurrencyRatesStore.actionCreators), dispatch)
        };
    }
    //TransactionsStore.actionCreators // Selects which action creators are merged into the component's props
)(Transactions as any);
