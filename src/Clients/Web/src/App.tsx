import React from 'react';
import logo from './logo.svg';
import './App.css';
import Button from '@mui/material/Button';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { LoginPage } from './pages/login';
import { LayoutPage } from './pages/layout';
import { Provider } from 'react-redux';
import store from './redux/store';

const theme = createTheme({
  palette: {
    mode: 'light'
  }
});

function App() {

  function showPage() {
    if (localStorage.getItem("loginToken")) {
      return <LayoutPage />;
    } else {
      return <LoginPage />;
    }
  }

  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        {showPage()}
      </ThemeProvider>
    </Provider>
  );
}

export default App;
