import React from 'react';
import {useState, useffect} from 'react';
import { Form, Button} from 'react-bootstrap';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
//import action
import {signupUser} from '../../actions/authActions';




const Signup = (props) => {

  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [Password, setPassword] = useState("");
  const [Email, setEmail] = useState("");


  const handleSubmit = (e) => {
    //destructure e.target.name and e.target.value
   e.preventDefault();
   //console.log("submitted");
   //console.log(FirstName);
   const newUser = {
     FirstName: FirstName,
     LastName: LastName,
     Email: Email,
     Password: Password
   };

   //register user
   //console.log(newUser);
   props.signupUser(newUser);
  }
  return(
    <Form onSubmit={handleSubmit}>

    <Form.Group controlId="formBasicEmail">
    <Form.Label>First Name</Form.Label>
    <Form.Control type="text" onChange={e => setFirstName(e.target.value)} name="FirstName" placeholder="first name" />
   
  </Form.Group>

    <Form.Group controlId="formBasicEmail">
    <Form.Label>Last Name</Form.Label>
    <Form.Control type="text" onChange={e => setLastName(e.target.value)} name="LastName" placeholder="Last name" />
   
  </Form.Group>

  <Form.Group controlId="formBasicEmail">
    <Form.Label>Email address</Form.Label>
    <Form.Control type="email" onChange={e => setEmail(e.target.value)}  name="Email" placeholder="email" />
    
  </Form.Group>

  <Form.Group controlId="formBasicPassword">
    <Form.Label>Password</Form.Label>
    <Form.Control type="password" onChange={e => setPassword(e.target.value)} name="Password" placeholder="Password" />
  </Form.Group>
  
  <Button variant="primary" type="submit">
    Submit
  </Button>
</Form>
  );
}


Signup.propTypes = {
  signupUser: PropTypes.func.isRequired,
  auth: PropTypes.object
}

//map state (i.e the state in the store) to the 'props' of this component
const mapStateToProps = (state) => ({
  authentication: state.auth //map this.props.authenctication (of this component) to state.auth - ref to authReducer in reducer/index.js
  //this is accessible via this.props.authentication 
});

//export default Signup;

export default connect(mapStateToProps, {signupUser})(withRouter(Signup));