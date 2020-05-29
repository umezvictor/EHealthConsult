import React from 'react';
import {useState, useffect} from 'react';


const Signup = (initialFieldValues) => {


  const [values, setValues] = useState(initialFieldValues);

  const handleChange = e => {
    const {name, value} = e.target;
    setValues({
      ...values,
      [name]: value
    })
  }

  return(
    values,
    setValues,
    handleChange
    
  );
}

export default Signup;