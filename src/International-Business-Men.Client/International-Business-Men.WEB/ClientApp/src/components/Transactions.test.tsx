import * as React from 'react';
import { Provider } from 'react-redux';
import * as renderer from 'react-test-renderer';
import { Transactions } from './Transactions';
import { MemoryRouter } from 'react-router-dom';
import configureStore from 'redux-mock-store';
import { currencyRates, transactions } from '../../test/setup/test-data.json';
import thunk from 'redux-thunk';
import { createMemoryHistory, createLocation } from 'history';
import { match } from 'react-router';

const history = createMemoryHistory();
const path = `/Transactions/:sku`;

const matcher: match<{ sku: string }> = {
    isExact: false,
    path,
    url: path.replace(':sku', '1'),
    params: { sku: "1" }
};

const middlewares = [thunk];
const mockStore = configureStore(middlewares);
const location = createLocation(matcher.url);

describe('', () => {
    let store;
    let component;
    let isLoading = false;

    beforeEach(() => {
        store = mockStore({
            currencyRates, transactions, isLoading
        });
        component = renderer.create(
            <Provider store={store}>
                <MemoryRouter>
                    <Transactions {...store} currencyRates={currencyRates} transactions={transactions}
                        isLoading={isLoading} requestCurrencyRates={jest.fn()} requestTransactions={jest.fn("")}
                        history={history} location={location} match={matcher}  />
                </MemoryRouter>                
            </Provider>
        );
    });

    it('renders Transactions component witouth crashing and to match snapshot', () => {
        expect(component.toJSON()).toMatchSnapshot();
    });
});

