import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Link, Route, Routes, useNavigate, useSearchParams } from 'react-router-dom';
import { api } from './api/client';

const tenantId = '00000000-0000-0000-0000-000000000001';
const Page = ({ title, children }) => <div style={{fontFamily:'Arial',maxWidth:780,margin:'2rem auto'}}><h1>{title}</h1>{children}</div>;
const Landing = () => <Page title='Cloud Solution Factory'><p>Start here.</p><Link to='/login'>Login</Link> | <Link to='/register'>Create account</Link></Page>;
function Register(){ const nav=useNavigate(); async function submit(e){e.preventDefault(); const fd=new FormData(e.target); const r=await api.registerEmail({tenantId,email:fd.get('email'),password:fd.get('password')}); localStorage.setItem('verificationLink',r.verificationLink); nav('/verification-pending');} return <Page title='Create account'><form onSubmit={submit}><input name='email' placeholder='Email' required/> <input name='password' placeholder='Password' type='password' required/> <button>Create</button></form></Page>}
function Login(){const nav=useNavigate(); async function submit(e){e.preventDefault(); const fd=new FormData(e.target); const r=await api.loginEmail({tenantId,email:fd.get('email'),password:fd.get('password')}); localStorage.setItem('demoToken',r.accessToken||''); nav('/dashboard');} return <Page title='Login'><form onSubmit={submit}><input name='email' required/> <input name='password' type='password' required/> <button>Login</button></form></Page>}
const Pending=()=> <Page title='Verification pending'><p>Check your email. Dev link: {localStorage.getItem('verificationLink')}</p></Page>;
function Verify(){const [q]=useSearchParams(); const [msg,setMsg]=React.useState('Verifying...'); React.useEffect(()=>{api.verifyEmail(q.get('token')).then(r=>setMsg(r.message)).catch(()=>setMsg('Verification failed'));},[]); return <Page title='Verify email result'><p>{msg}</p></Page>}
function Dashboard(){const [me,setMe]=React.useState(null); React.useEffect(()=>{api.me(localStorage.getItem('demoToken')).then(setMe).catch(()=>setMe({email:'unknown'}));},[]); return <Page title='Welcome Dashboard'><p>Welcome {me?.email}</p></Page>}
createRoot(document.getElementById('root')).render(<BrowserRouter><Routes><Route path='/' element={<Landing/>}/><Route path='/register' element={<Register/>}/><Route path='/login' element={<Login/>}/><Route path='/verification-pending' element={<Pending/>}/><Route path='/verify-email' element={<Verify/>}/><Route path='/dashboard' element={<Dashboard/>}/></Routes></BrowserRouter>);
