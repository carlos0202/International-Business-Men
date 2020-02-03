import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import CurrencyRates from './components/CurrencyRates';
import Transactions from './components/Transactions';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/currency-rates' component={CurrencyRates} />
        <Route path='/transactions/:sku?' component={Transactions} />
    </Layout>
);
