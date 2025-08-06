<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=00FF00&height=120&section=header"/>

# SCFP - Sistema de Controle Financeiro Pessoal


## 💼 Sobre o Projeto

O **SCFP** é um sistema desenvolvido para auxiliar pessoas no controle das finanças pessoais, proporcionando uma forma prática, intuitiva e segura de acompanhar receitas e despesas, categorizar gastos e visualizar relatórios financeiros completos.

---

### 🔐 Funcionalidades

- Tela de **login com verificação em duas etapas (2FA)**
- **Cadastro de usuários** com segurança
- **Criação de categorias** personalizadas (Ex: Alimentação, Transporte, Lazer...)
- **Registro de transações** (entradas e saídas financeiras com valor, data e categoria)
- **Resumo mensal** com total de receitas, despesas e saldo
- **Relatórios e gráficos** financeiros detalhados
- **Atualização de perfil** com edição de dados, senha e foto

---

### 📘 Tutorial Rápido

**Bem-vindo ao SCFP!**

> O SCFP ajuda você a organizar suas finanças de forma simples e segura. Aqui está como começar:

1. **Registrar sua conta**: Faça seu cadastro inicial.
2. **Adicionar categorias**: Personalize seus tipos de gastos.
3. **Registrar transações**: Insira entradas e saídas com detalhes.
4. **Visualizar resumo mensal**: Acompanhe seu saldo do mês.
5. **Acessar relatórios**: Entenda seus hábitos através de gráficos.
6. **Atualizar seu perfil**: Gerencie seus dados pessoais.

---

### 🛠️ Tecnologias utilizadas

- HTML5 / CSS3 / JavaScript
- ASP.NET Core MVC com Identity
- Entity Framework Core (EF)
- Bootstrap
- Chart.js

---

### 📅 Cronograma de Desenvolvimento

#### ✅ Semana 1: Planejamento e Estrutura Inicial
- Definir os requisitos funcionais e não funcionais
- Criar os casos de uso
- Criar o diagrama de entidades (usuário, transações, categorias)
- Criar o projeto no Visual Studio
- Configurar o ASP.NET Core MVC com Identity
- Criar as entidades/models iniciais (Usuario, Categoria, Transacao)
- Criar o banco de dados com Entity Framework Core (EF)

#### ✅ Semana 2: Funcionalidades de Cadastro
- CRUD de Categorias (alimentação, transporte, etc.)
- CRUD de Transações (entrada e saída de valores)
- Validar entradas de dados
- Associar dados ao usuário logado
- Exibir resumo mensal (ex: total de entradas e saídas)

#### ✅ Semana 3: Relatórios e Gráficos
- Criar tela de relatórios
- Usar Chart.js para exibir:
    - Entradas vs Saídas por mês
    - Gráfico de pizza por categoria
- Permitir filtro por mês ou intervalo de datas

#### ✅ Semana 4: Segurança, Refino e Testes
- Implementar autenticação por dois fatores (2FA) via e-mail ou app autenticador
- Revisar o código, refatorar onde necessário
- Testar as funcionalidades (testes manuais ou unitários simples)
- Estilizar as views com Bootstrap
- Documentar o projeto (README ou documento técnico)

---

### 💡 Extras e Melhorias Futuras

- Exportar relatórios para PDF
- Adicionar alertas visuais (ex: Toasts, Bootstrap Alerts)
- Publicar na nuvem (ex: Azure ou Railway)

---



<img width=100% src="https://capsule-render.vercel.app/api?type=waving&color=00FF00&height=120&section=footer"/>
