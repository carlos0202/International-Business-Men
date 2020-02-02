import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as TransactionsStore from '../store/Transactions';
import { RouteComponentProps } from 'react-router';

// At runtime, Redux will merge together...
type TransactionProps =
    TransactionsStore.TransactionsState // ... state we've requested from the Redux store
    & typeof TransactionsStore.actionCreators // ... plus action creators we've requested
    & RouteComponentProps<{ sku: string }>;


class Transactions extends React.PureComponent<TransactionProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Transacciones</h1>
                <p>Esta es la tabla de transacciones almacenadas el proveedor de datos actual.</p>
                <br />
                {this.renderTransactionsTable()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        const sku = this.props.match.params.sku || "";
        this.props.requestTransactions(sku);
    }

    private renderTransactionsTable() {
        let index = 1;
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Identificador Producto</th>
                        <th>Moneda</th>
                        <th>Monto Transacci&oacute;n</th>
                    </tr>
                </thead>
                <tbody>
                    {this.props.transactions.map((transaction: TransactionsStore.Transaction) =>
                        <tr key={index++}>
                            <td>{transaction.sku}</td>
                            <td>{transaction.currency}</td>
                            <td>{transaction.amount}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.transactions, // Selects which state properties are merged into the component's props
    TransactionsStore.actionCreators // Selects which action creators are merged into the component's props
)(Transactions as any);
