const base = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5254';
async function req(path, opts={}){ const r=await fetch(`${base}${path}`,{headers:{'Content-Type':'application/json',...(opts.token?{Authorization:`Bearer ${opts.token}`}:{})},...opts}); if(!r.ok) throw new Error('Request failed'); return r.json(); }
export const api={
 registerEmail:(body)=>req('/auth/register/email',{method:'POST',body:JSON.stringify(body)}),
 loginEmail:(body)=>req('/auth/login/email',{method:'POST',body:JSON.stringify(body)}),
 verifyEmail:(token)=>req(`/auth/verify-email?token=${encodeURIComponent(token||'')}`),
 resendVerification:(body)=>req('/auth/resend-verification',{method:'POST',body:JSON.stringify(body)}),
 me:(token)=>req('/me',{token})
};
