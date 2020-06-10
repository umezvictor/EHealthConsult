import React from 'react';
import {Form, Button} from 'react-bootstrap';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
import {useState, useffect} from 'react';
//import action
import {loginUser} from '../../actions/authActions';

const Login = (props) => {

  const [Email, setEmail] = useState("");
  const [Password, setPassword] = useState("");

  const handleSubmit = (e) => {

    e.preventDefault();

    const userCredentials = {
      Email: Email,
      Password: Password
    };

    props.loginUser(userCredentials);

  }
  
  return(
    <Form onSubmit={handleSubmit}>
    <Form.Group controlId="formBasicEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="email" onChange={e => setEmail(e.target.value)} name="Email" placeholder="Enter email" />
       
    </Form.Group>

    <Form.Group controlId="formBasicPassword">
    <Form.Label>Password</Form.Label>
    <Form.Control type="password" onChange={e => setPassword(e.target.value)} name="Password" placeholder="Password" />
  </Form.Group>

  <Button variant="primary" type="submit">
    Login
  </Button>
</Form>
  );
   
}


Login.propTypes = {
  loginUser: PropTypes.func.isRequired,
  auth: PropTypes.object
}

const mapStateToProps = (state) => ({
  authentication: state.auth
});

export default connect(mapStateToProps, {loginUser})(withRouter(Login));