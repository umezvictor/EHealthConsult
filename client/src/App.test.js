import React from 'react';
import { render } from '@testing-library/react';
import {shallow} from 'enzyme/build';
import App from './App';

test('renders learn react link', () => {
  const { getByText } = render(<App />);
  const linkElement = getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
 // const wrapper = shallow(<App />);
 // wrapper.unmount()
});
