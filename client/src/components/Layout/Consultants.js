import React, { Fragment } from 'react';
import {useState, useEffect} from 'react';
import {withRouter} from 'react-router-dom';
import PropTypes from 'prop-types';
//connect connects your componen to the store that wad provided by the Provider component
import {connect} from 'react-redux';
import {Link} from 'react-router-dom';
//import action
import {getConsultants} from '../../actions/consultantActions';


const Consultants = (props) => {

//componentDidMount --runs when the component renders
    useEffect(() => {
            props.getConsultants();          
        }, []);
    
        let itemsList;
        itemsList = (props.consultantRecords.consultants.map((consultant) => 
            <React.Fragment key={consultant.id}>
                <ul>
                <li>{consultant.firstName}</li>
                    <li>
                    <Link to={{ 
                        pathname: '/appointment',
                        consultantId: `${consultant.id}`
                    }}>
                        Book appointment </Link>
                        </li>
                    <li>{consultant.lastName}</li>
                    <li>{consultant.phone}</li>
                    <li>{consultant.profession}</li>
                    <li>{consultant.yearsOfExperience}</li>
                </ul>
                <div>
                    <img width="200px" height="200px" src={`http://localhost:5000/api/consultants/getimage/${consultant.id}`} alt={consultant.firstName}/>
                </div>
            </React.Fragment>
        ))


    return(       
        <div>
             <h4>Consultants</h4>      
            {itemsList}            
        </div>      
    );
};


//add isrequired later
Consultants.propTypes = {
    getConsultants: PropTypes.func.isRequired,
    auth: PropTypes.object,
    consultantRecords: PropTypes.object
}

const mapStateToProps = (state) => ({
    auth: state.auth,
    consultantRecords: state.consultant //maps to consultantReducer -- consultant in combineReducers
});

export default connect(mapStateToProps, {getConsultants})(withRouter(Consultants));

