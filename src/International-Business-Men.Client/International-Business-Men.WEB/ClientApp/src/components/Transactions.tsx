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
    InputGroupText,
    FormGroup,
    Label,
    Row,
    Col,
    Card,
    CardTitle
} from 'reactstrap';
import { roundNumber, getConversionAmount } from '../utils/math';
import { bindActionCreators } from 'redux';
import TransactionsTable from './TransactionsTable';
import './Transactions.css';

// At runtime, Redux will merge together...
type TransactionProps =
    TransactionsStore.TransactionsState
    & CurrencyRatesStore.CurrencyRatesState// ... state we've requested from the Redux store
    & typeof TransactionsStore.actionCreators // ... plus action creators we've requested
    & typeof CurrencyRatesStore.actionCreators
    & RouteComponentProps<{ sku: string }>;

class Transactions extends React.PureComponent<TransactionProps, {}, { sku: string, convertCurrency: string }> {
    public state = {
        sku: '',
        convertCurrency: 'EUR'
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
            <Card body>
                <CardTitle><h1>Transacciones</h1></CardTitle>
                <Row>
                    <Col md="9">
                        <Form onSubmit={this.onSubmit.bind(this)}>
                            <InputGroup>
                                <Input type="text" id="sku" onChange={this.onChange.bind(this)} placeholder="SKU del Producto" />
                                <InputGroupAddon addonType="append" onClick={this.onSubmit.bind(this)} className="button-hover">
                                    <InputGroupText>Buscar</InputGroupText>
                                </InputGroupAddon>
                            </InputGroup>
                        </Form>
                    </Col>
                    <Col md="3">
                        <InputGroup>
                            <Input type="select" name="convertCurrency" id="convertCurrency" placeholder="Moneda de Conversión">
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </Input>
                        </InputGroup>
                    </Col>
                </Row>
                
                <br />
                <br />
                <TransactionsTable {...this.props} convertCurrency={this.state.convertCurrency} />
            </Card>
        );
    }

    private ensureDataFetched() {
        this.props.requestCurrencyRates();
        const sku = this.props.match.params.sku || "";
        this.props.requestTransactions(sku);        
    }

    private convertTransactions(convertedCurrency: string): TransactionsStore.ConvertedTransaction[] {
        let { transactions, currencyRates } = this.props;

        return transactions.map((transaction: TransactionsStore.Transaction) => {
            let conv = getConversionAmount(transaction, convertedCurrency, currencyRates);

            return conv ? conv : { ...transaction, conversionRate: 0, convertedAmount: -1, convertedCurrency: convertedCurrency };
        });
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
