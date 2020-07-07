import React from 'react';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';

const Payment = (props) => {
    console.log(props.appointment)
    return(
        <h1>Make your payment here</h1>
    );
};


Payment.propTypes = {
    appointment: PropTypes.object
  }
  
  //map state (i.e the state in the store) to the 'props' of this component
  const mapStateToProps = (state) => ({
    //auth: state.auth,
    appointment: state.appointment
  });
  
  //export default Appointment;
  
export default connect(mapStateToProps)(withRouter(Payment));

//export default Payment;