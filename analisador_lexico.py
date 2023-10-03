class Token:
    classe=""
    valor=""
    def __init__(self, valor, classe):
        self.valor = valor
        self.classe = classe

estado=1
cadeia="(A12345+(a1+22)*42)"

valor=""
i=0 # ponteiro inicial
global pos
pos=0
global lista
lista=[]
while (i<len(cadeia)):
    if (estado==1): #identificador
        #print("Estado 1")
        if (cadeia[i:i+1]=="(" or cadeia[i:i+1]==")" or cadeia[i:i+1]=="+" or cadeia[i:i+1]=="*" or cadeia[i:i+1]=="/"):
            estado=2
            continue
        elif (cadeia[i:i+1].isdigit()):
            estado=3
            continue
        # Agora entendi o que é um loop para uma AFD
        elif (cadeia[i:i+1].isalpha() or cadeia[i:i+1].isdigit()):
             while (cadeia[i:i+1].isalpha() or cadeia[i:i+1].isdigit()):
                 valor=valor+cadeia[i:i+1]
                 Token.classe="ide"
                 Token.valor=valor
                 i=i+1
        else:
             estado=99
             continue
    elif (estado==2): #simbolo
         #print("Estado 2")
         Token.valor=cadeia[i:i+1]
         Token.classe="sim"
    elif (estado==3): #numero
        #print("Estado 3")
        while (cadeia[i:i+1].isdigit()):
            valor=valor+cadeia[i:i+1]
            Token.classe="num"
            Token.valor=valor
            i=i+1
    elif (estado==99): #fora da gramática
         #print("Estado 99")
         Token.valor=cadeia[i:i+1]
         Token.classe="out"
         print(Token.valor)
         print("Símbolo fora da gramática ! Análise léxica não OK!")
         raise SystemExit
    print(Token.classe+" "+Token.valor)
    tok=Token(Token.valor,Token.classe)
    lista.append(tok)
    valor=""
    estado=1
    
    if (Token.classe=="sim" or Token.classe=="out"):
        i=i+1

print("análise léxica OK!")
pos=0

               
