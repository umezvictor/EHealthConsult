//core ui admin template dependencies
import 'react-app-polyfill/ie11'; //for IE 11 support
import 'react-app-polyfill/stable';
import './components/admin/polyfill';
//end of dependencies

import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import * as serviceWorker from './serviceWorker';
import 'bootstrap/dist/css/bootstrap.min.css';


//core ui icons
import {icons} from './components/admin/assets/icons';

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);


serviceWorker.unregister();
