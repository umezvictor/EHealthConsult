import React from 'react';
import Navbar from './components/Layout/Navbar';
import Body from './components/Layout/Body';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import {Provider} from 'react-redux';//this is the binding between react and redux
import store from './store';

import Login from './components/Auth/Login';
import Signup from './components/Auth/Signup';


function App() {
  return (
    <Provider store={store}>
      <Router>
        <div className="App">
          <Navbar />
          
          <Route exact path="/"  />
          <Route exact path="/login" component={Login} />
          <Route exact path="/signup" component={Signup} />
          
        </div>
    </Router>
    </Provider>
    
    
  );
}

export default App;
