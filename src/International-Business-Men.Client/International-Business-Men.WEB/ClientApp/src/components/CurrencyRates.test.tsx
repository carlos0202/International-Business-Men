import * as React from 'react';
import { Provider } from 'react-redux';
import * as renderer from 'react-test-renderer';
import { CurrencyRates } from './CurrencyRates';
import configureStore from 'redux-mock-store';
import { currencyRates } from '../../test/setup/test-data.json';
import thunk from 'redux-thunk';

const middlewares = [thunk];
const mockStore = configureStore(middlewares);

describe('', () => {
    let store;
    let component;
    let isLoading = false;

    beforeEach(() => {
        store = mockStore({
            currencyRates, isLoading
        });
        component = renderer.create(
            <Provider store={store}>
                <CurrencyRates {...store} currencyRates={currencyRates}
                    isLoading={isLoading} requestCurrencyRates={jest.fn()} />
            </Provider>
        );
    });

    it('renders CurrencyRates component witouth crashing and to match snapshot', () => {
        expect(component.toJSON()).toMatchSnapshot();
    });
});


