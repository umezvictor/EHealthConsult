import React, { Fragment } from "react";
import {Navbar, Nav, NavDropdown} from 'react-bootstrap';

import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
import {Link} from 'react-router-dom';
import {logoutUser} from '../../actions/authActions';

const Navigation = (props) => {

  const handleLogout = (e) => {
    e.preventDefault();    
    props.logoutUser();
  };

const { isAuthenticated, user } = props.auth

  //guest links
  const guestLinks = (
    <Fragment>
    <Nav.Link href="/login">{user.FirstName}Login</Nav.Link>
    <Nav.Link href="/signup">Signup</Nav.Link>
    </Fragment>  
  );
  //destructure
  
console.log(props);
  //authenticated user links
  const authLinks = (
    <Fragment>
<Link to=" " onClick={handleLogout}>
        Welcome {user.FirstName} Logout
      </Link>
       <Link to="/appointment">
       Appointment
     </Link>
     <Link to="/consultant">
     Consultant
   </Link>
    </Fragment>
      
  );

 return(
    <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
    <Navbar.Brand href="/">E-Health Consult</Navbar.Brand>
    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
    <Navbar.Collapse id="responsive-navbar-nav">
      <Nav className="mr-auto">
       
       {isAuthenticated ? authLinks : guestLinks}
      </Nav>
      <Nav>
     
      
      </Nav>
    </Navbar.Collapse>
  </Navbar>
 );
}

//export default Navigation;
Navigation.propTypes = {
  logoutUser: PropTypes.func.isRequired,
  auth: PropTypes.object
}

//map state (i.e the state in the store) to the 'props' of this component
const mapStateToProps = (state) => ({
  auth: state.auth //map this.props.authenctication (of this component) to state.auth - ref to authReducer in reducer/index.js
  //this is accessible via this.props.authentication 
});

//export default Signup;

export default connect(mapStateToProps, {logoutUser})(withRouter(Navigation));