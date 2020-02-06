import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { fillCurrencyTable } from '../utils/transformations';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface CurrencyRatesState {
    isLoading: boolean;
    currencyRates: CurrencyRate[];
}

export interface CurrencyRate {
    from: string;
    to: string;
    rate: number;
}

export interface ResponseModel<T> {
    statusCode: number,
    statusMessage: string,
    result: T[];
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestCurrencyRatesAction {
    type: 'REQUEST_CURRENCY_RATES';
}

interface ReceiveCurrencyRatesAction {
    type: 'RECEIVE_CURRENCY_RATES';
    currencyRates: CurrencyRate[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestCurrencyRatesAction | ReceiveCurrencyRatesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestCurrencyRates: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();

        if (appState && appState.currencyRates) {
            fetch(`${process.env.REACT_APP_API_URL}/CurrencyRates`)
                .then(response => response.json() as Promise<ResponseModel<CurrencyRate>>)
                .then(data => {
                    const rates = fillCurrencyTable(data.result);
                    dispatch({ type: 'RECEIVE_CURRENCY_RATES', currencyRates: rates });
                });

            dispatch({ type: 'REQUEST_CURRENCY_RATES' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: CurrencyRatesState = { currencyRates: [], isLoading: false };

export const reducer: Reducer<CurrencyRatesState> = (state: CurrencyRatesState | undefined, incomingAction: Action): CurrencyRatesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CURRENCY_RATES':
            return {
                currencyRates: state.currencyRates,
                isLoading: true
            };
        case 'RECEIVE_CURRENCY_RATES':
            if (action.currencyRates) {
                return {
                    currencyRates: action.currencyRates,
                    isLoading: false
                };
            }
    }

    return state;
};
