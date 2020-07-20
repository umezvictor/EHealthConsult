import React from 'react';
import Navigation from './components/Layout/Navigation';
//import Body from './components/Layout/Body';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import {Provider} from 'react-redux';//this is the binding between react and redux
import store from './store';

import Login from './components/Auth/Login';
import Signup from './components/Auth/Signup';
import Index from './components/Layout/Index';
import Appointment from './components/Layout/Appointment';
import Payment from './components/Layout/Payment';
import Consultants from './components/Layout/Consultants';
import CreateConsultant from './components/Layout/CreateConsultant';

//core ui scss
import './components/admin/scss/style.scss';

const loading = (
  <div className="pt-3 text-center">
    <div className="sk-spinner sk-spinner-pulse"></div>
  </div>
);

//core ui layout
const TheLayout = React.lazy(() => import('./components/admin/containers/TheLayout'));

function App() {
  return (
    <Provider store={store}>
      <Router>
      <React.Suspense fallback={loading}>
        <Switch>
        <div className="App">
          <Route exact path="/login" component={Login} />
          <Route exact path="/signup" component={Signup} />
          <Route exact path="/appointment" component={Appointment} />
          <Route exact path="/consultants" component={Consultants} />
          <Route exact path="/addConsultant" component={CreateConsultant} />
          <Route exact path="/home" component={Index} />
          <Route exact path="/payment" component={Payment} />
          <Route path="/" name="Home" render={props => <TheLayout {...props}/>} />
        </div>
        </Switch>
        </React.Suspense>
    </Router>
    </Provider>
       
  );
}
//<Navigation /> 
//<Route exact path="/admin" name="Admin" render={props => <AdminLayout {...props}/>} />
export default App;
