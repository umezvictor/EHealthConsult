import React from 'react';
import {useState, useffect} from 'react';
import { Form, Button} from 'react-bootstrap';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
//import action
import {bookAppointment} from '../../actions/appointmentActions';


  const Appointment = (props) => {

  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [Phone, setPhone] = useState("");
  const [Gender, setGender] = useState("");
  const [Email, setEmail] = useState("");
  const [DateOfBirth, setDateOfBirth] = useState("");
  const [Complaint, setComplaint] = useState("");


  const handleSubmit = (e) => {
    //destructure e.target.name and e.target.value
   e.preventDefault();
  
   const appointmentDetails = {
      FirstName: FirstName,
      LastName: LastName,
      Email: Email,
      Phone: Phone,
      Gender: Gender,
      DateOfBirth: DateOfBirth,
      Complaint: Complaint  
   };

   
   //book appointment
   //props.location.consultantId   - consultantId is received from Consultants.as props
   props.bookAppointment(appointmentDetails, props.location.consultantId, props.history);
  // console.log(props.appointment)
   
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

  <Form.Group >
    <Form.Label>Phone</Form.Label>
    <Form.Control type="text" onChange={e => setPhone(e.target.value)} name="Phone" placeholder="Phone" />
  </Form.Group>
  
  <Form.Group controlId="formBasicPassword">
    <Form.Label>Gender</Form.Label>
    <Form.Control type="text" onChange={e => setGender(e.target.value)} name="Gender" placeholder="Gender" />
  </Form.Group>
  ,
      Complaint: 
  <Form.Group controlId="formBasicPassword">
    <Form.Label>DateOfBirth</Form.Label>
    <Form.Control type="text" onChange={e => setDateOfBirth(e.target.value)} name="DateOfBirth" placeholder="DateOfBirth" />
  </Form.Group>

  <Form.Group>
    <Form.Label>Complaint</Form.Label>
    <Form.Control type="text" onChange={e => setComplaint(e.target.value)} name="Complaint" placeholder="Complaint" />
  </Form.Group>

  <Button variant="primary" type="submit">
    Submit
  </Button>
</Form>
  );
}


Appointment.propTypes = {
  bookAppointment: PropTypes.func.isRequired,
  auth: PropTypes.object,
  appointment: PropTypes.object
}

//map state (i.e the state in the store) to the 'props' of this component
const mapStateToProps = (state) => ({
  auth: state.auth,
  appointment: state.appointment
});

//export default Appointment;

export default connect(mapStateToProps, {bookAppointment})(withRouter(Appointment));
