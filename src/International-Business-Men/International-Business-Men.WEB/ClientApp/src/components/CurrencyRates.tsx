import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as CurrencyRatesStore from '../store/CurrencyRates';

// At runtime, Redux will merge together...
type CurrencyRateProps =
    CurrencyRatesStore.CurrencyRatesState // ... state we've requested from the Redux store
    & typeof CurrencyRatesStore.actionCreators; // ... plus action creators we've requested


class CurrencyRates extends React.PureComponent<CurrencyRateProps> {
    // This method is called when the component is first added to the document
    public componentDidMount() {
        this.ensureDataFetched();
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
        this.ensureDataFetched();
    }

    public render() {
        return (
            <React.Fragment>
                <h1 id="tabelLabel">Weather forecast</h1>
                <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
                {this.renderCurrencyRatesTable()}
            </React.Fragment>
        );
    }

    private ensureDataFetched() {
        this.props.requestCurrencyRates();
    }

    private renderCurrencyRatesTable() {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
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
        );
    }
}

export default connect(
    (state: ApplicationState) => state.CurrencyRates, // Selects which state properties are merged into the component's props
    CurrencyRatesStore.actionCreators // Selects which action creators are merged into the component's props
)(CurrencyRates as any);
