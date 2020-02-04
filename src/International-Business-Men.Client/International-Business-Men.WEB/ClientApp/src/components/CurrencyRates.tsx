import * as React from 'react';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as CurrencyRatesStore from '../store/CurrencyRates';
import {
    Card,
    CardTitle
} from 'reactstrap';
import LoadingSpinner from './LoadingSpinner';

// At runtime, Redux will merge together...
type CurrencyRateProps =
    CurrencyRatesStore.CurrencyRatesState // ... state we've requested from the Redux store
    & typeof CurrencyRatesStore.actionCreators; // ... plus action creators we've requested


class CurrencyRates extends React.PureComponent<CurrencyRateProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    public render() {
        const { isLoading } = this.props;
        return (
            <Card body>
                <CardTitle><h1 id="tabelLabel">Conversiones</h1></CardTitle>

                <br />
                {(isLoading || !this.props.currencyRates) ?
                    <div><LoadingSpinner /></div> :
                    <div>{this.renderCurrencyRatesTable()}</div>
                }
            </Card>
        );
    }

    private ensureDataFetched() {
        this.props.requestCurrencyRates();
    }

    private renderCurrencyRatesTable() {
        return (
            <div className="table-responsive-md">
                <table className='table table-striped table-bordered table-hover' aria-labelledby="currencyTableLabel">
                    <thead className="thead-dark">
                        <tr>
                            <th>Moneda Fuente</th>
                            <th>Moneda Destino</th>
                            <th>Valor de Conversi&oacute;n</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.currencyRates.map((currencyRate: CurrencyRatesStore.CurrencyRate) =>
                            <tr key={`${currencyRate.from}-${currencyRate.to}`}>
                                <td>{currencyRate.from}</td>
                                <td>{currencyRate.to}</td>
                                <td>{currencyRate.rate}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.currencyRates, // Selects which state properties are merged into the component's props
    CurrencyRatesStore.actionCreators // Selects which action creators are merged into the component's props
)(CurrencyRates as any);
