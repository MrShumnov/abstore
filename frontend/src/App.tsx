import React from 'react';
import { Routes, Route } from "react-router-dom";
import './App.css';

import Navbar from './components/navbar';

import Login from './pages/login';
import Register from './pages/register';
import Store from './pages/store/store';

function App() {
  return (
    <div className="App">
      <Navbar/>
      
      <div>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/store" element={<Store/>} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
