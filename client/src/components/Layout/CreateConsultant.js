import React from 'react';
import {useState} from 'react';
import { Form, Button} from 'react-bootstrap';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
//import action
import {addConsultant} from '../../actions/consultantActions';


  const CreateConsultant = (props) => {

  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [Email, setEmail] = useState("");
  const [YearsOfExperience, setYearsOfExperience] = useState("");
  const [Phone, setPhone] = useState("");
  const [Profession, setProfession] = useState("");
  //const [ProfilePicture, setProfilePicture] = useState("");


  const handleSubmit = (e) => {
    //destructure e.target.name and e.target.value
   e.preventDefault();
      
   
   //file upload
  // const data = new FormData();
   // data.append('ProfilePicture', ProfilePicture);

   const consultantData = {
      FirstName: FirstName,
      LastName: LastName,
      Email: Email,
      YearsOfExperience: YearsOfExperience,
      Phone: Phone,
      Profession: Profession
   };

   //console.log(consultantDetails);
   //book appointment
   //props.location.consultantId   - consultantId is received from Consultants.as props
   props.addConsultant(consultantData);
  // console.log(props.appointment)
   
  }
  return(
    <Form  onSubmit={handleSubmit}>

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
    <Form.Label>Years of Experience</Form.Label>
    <Form.Control type="text" onChange={e => setYearsOfExperience(e.target.value)} name="YearsOfExperience" placeholder="YearsOfExperience" />
  </Form.Group>
  
  <Form.Group controlId="formBasicPassword">
    <Form.Label>Phone</Form.Label>
    <Form.Control type="text" onChange={e => setPhone(e.target.value)} name="Phone" placeholder="Phone" />
  </Form.Group>
 
  <Form.Group controlId="formBasicPassword">
    <Form.Label>Profession</Form.Label>
    <Form.Control type="text" onChange={e => setProfession(e.target.value)} name="Profession" placeholder="Profession" />
  </Form.Group>

  <Button variant="primary" type="submit">
    Submit
  </Button>
</Form>
  );
}


CreateConsultant.propTypes = {
  addConsultant: PropTypes.func.isRequired,
  auth: PropTypes.object,
  consultant: PropTypes.object
}

//map state (i.e the state in the store) to the 'props' of this component
const mapStateToProps = (state) => ({
  auth: state.auth,
  consultant: state.consultant
});


export default connect(mapStateToProps, {addConsultant})(withRouter(CreateConsultant));
