def remove_char(s)
  a = s.slice(1,s.length-2)
  return a
end

def remove_char_better(q)
  puts q.slice(1..-2)
end

puts remove_char("Hello World!")
remove_char_better("Hello World!")